app.controller('contractorEditProfileController', ['$scope', 'dataService', '$rootScope','toastr', function ($scope, db, $rootScope,toastr) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Edit Profile";
    $scope.loadSkills = db.system.getSkills;

    $scope.refreshAddresses = function (address) {
        return db.system.searchLocations(address).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };

    var ignoreList = ['rss','locationCode','profile','companyId'];

    function calculateProgress(newVal) {
        $scope.pendings = [];
        var total = 0, pg = 0;
        for (var x in newVal) {
            if (ignoreList.indexOf(x) > -1) continue;
            if (angular.isArray(newVal[x]) && (newVal[x] && newVal[x].length > 0)) {
                pg++;
            } else if (newVal[x]) {
                pg++;
            } else {
                $scope.pendings.push(x);
            }
            total++;
        }
        var complete = Math.round(pg / total * 100, 0);
        $scope.status = complete == 100 ? 'success' : complete < 20 ? 'danger' : complete > 20 && complete < 50 ? 'warning' : 'info';
        $scope.complete = complete;
        if (newVal && newVal.complete != complete) {
            newVal.complete = complete;
        }
    }

    $scope.$watch('record', calculateProgress, true);

    $scope.save = function (record) {
        if (record.mobile == record.alternateNumber && record.mobile) {
            toastr.error('Mobile and Mobile 2 Number can\'t be same.', 'Validation');
            return;
        }
        if (record.cv) {
            record.profileUrl = record.cv.url;
        }
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }
        record.experienceYears = Math.floor(record.experience / 12);
        record.experienceMonths = record.experience % 12;
        calculateProgress(record);
        console.log('Progress', record.complete);
        db.contractor.update(record).success(function (result) {
            window.location = "#/profile";
        });
    }
    $scope.addSkill = function (skills, level) {
        for (var x in skills) {
            $scope.record.skills.push({ level: level.id, proficiency: "Beginer", experienceInMonths: 0, code: skills[x].code, title: skills[x].title });
        }
        $scope.newSkill = [];
    }
    $scope.remove = function (item) {
        var index = $scope.record.skills.indexOf(item);
        $scope.record.skills.splice(index, 1);
    }

    function preloader(func, prop) {
        return function () {
            return func().success(function (result) {
                $scope[prop] = result;
            });
        }
    }
    function preloaderenum(func,name, prop) {
        return function () {
            return func(name).then(function (result) {
                $scope[prop] = result;
            });
        }
    }

    function getMasters() {
        return preloader(db.system.getCountries, 'nations')()
            .then(preloader(db.system.getLocations, 'locations'))
            .then(preloaderenum(db.system.enums, 'proficiencyEnum', 'proficiencies'))
            .then(preloaderenum(db.system.enums, 'levelEnum', 'levels'))
            .then(preloaderenum(db.system.enums, 'contractorTypeEnum', 'contractorType'))
            .then(preloaderenum(db.system.enums, 'contractTypeEnum', 'contractType'))
            .then(preloaderenum(db.system.enums, 'genderEnum', 'genders'));
    }

    function navigate() {
        return db.contractor.get().success(function (result) {
            //result.companyId = !result.companyId ? 1 : result.companyId; //TODO:remove this line
            result.picture = {
                url: result.pictureUrl
            };
            result.loc = {
                formatted_address: result.location
            };
            result.experience = (result.experienceYears * 12) + result.experienceMonths;
            $scope.record = result;
        });
    }
    function loadSchedule() {
        db.contractor.getSchedule().success(function(result) {
            $scope.record.schedules = result;
        });
    }
    

    $scope.experienceTranslate = function (value) {
        if (value == 0) return 'Fresher';
        var years = Math.floor(value / 12);
        var months = value % 12;
        return years + (months > 0 ? "." + months : "") + 'y';
    }


    $scope.deleteSchedule = function (s, i) {
        db.contractor.deleteSchedule(s).success(loadSchedule);
    };


    $scope.saveSchedule = function (record) {
        db.contractor.createSchedule({
            start: record.date.startDate.format(),
            end: record.date.endDate.format(),
            company: record.company,
            isAvailable: record.isAvailable
        })
        .success(loadSchedule)
        .error(function (err) {
            toastr.error(err.exceptionMessage, err.message);
        }).then(function () {
            $scope.start = '';
            $scope.end = '';
        });
    }


    $scope.genders = db.system.genders;

    db.system.getFunctionals().success(function (data) {
        $scope.functionalAreas = data;
    });
    db.system.getIndustries().success(function (data) {
        $scope.industries = data;
    });
    db.system.getCountries().success(function (data) {
        $scope.nations = data;
    });

    navigate().then(getMasters);

}]);