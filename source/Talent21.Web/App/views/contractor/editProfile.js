app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    $scope.loadSkills = db.system.getSkills;

    $scope.refreshAddresses = function (address) {
        return db.system.searchLocations(address).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };

    $scope.save = function (record) {
            if (record.cv) {
            record.profileUrl = record.cv.url;
        }
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }

        db.contractor.update(record).success(function (result) {
            window.location = "#/profile";
        });
    }

    $scope.addSkill = function (skills, level) {
        for (var x in skills) {
            $scope.record.skills.push({ level: level, proficiency: "Beginer", experienceInMonths: 0, code: skills[x].code, title: skills[x].title});
        }
        $scope.newSkill = [];
    }
    $scope.remove = function (item) {
        var index = $scope.record.skills.indexOf(item);
        $scope.record.skills.splice(index, 1);
    }

    function getMasters() {
        db.system.getLocations().success(function (result) {
            $scope.locations = result;
        });

        db.system.enums('proficiencyEnum').then(function (enums) {
            $scope.proficiencies = enums;
            db.system.enums('levelEnum').then(function (levels) {
                $scope.levels = levels;
            });
        });
    }

    function navigate() {
        db.contractor.get().success(function (result) {
            result.picture = {
                url: result.pictureUrl
            };
            result.loc = {
                formatted_address: result.location
            };
            $scope.record = result;
        });
    }

    getMasters();
    navigate();

}]);