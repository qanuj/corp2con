app.controller('companySearchController', ['$scope', 'dataService','$rootScope',function ($scope, db,  $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Contractor : Search Result";
    $scope.save = "Save";

    var param = db.args();

    if (!isNaN(param.idea)) {
        param.page = page.idea;
    } else if (param.idea == "match") {
        $scope.searching = "Matching Jobs for you.";
    } else if (param.idea == "month") {
        $scope.searching = "Matching Jobs for you, next month";
    } else if (param.idea == "week") {
        $scope.searching = "Matching Jobs for you, next week";
    }

    $scope.query = {
        keywords: param.q || param.keywords || '',
        location: param.location || '',
        folder: param.idea || param.folder || '',
        industry: param.industry || '',
        functional: param.functional || '',
        skills: param.skills || '',
        ratestart: param.ratestart || '',
        rateend: param.rateend || '',
        xfrom: param.xfrom || '',
        xto: param.xto || '',
        ratetype: param.ratetype || ''
    }

    console.log("Searching", $scope.query);

    function fetchResults(query, page) {
        db.company.search(query, page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
            $scope.page = page;
            $scope.selectAll = false;
        });
    }

    $scope.search = function (query) {
        var q = '';
        for (var x in query) {
            q += (q === '' ? '?' : '&') + x + '=' + query[x];
        }
        fetchResults(query, 1);
        window.location = '#/search' + q; //this is decoration for URL only.
    }

    $scope.navigate = function (page) {
        fetchResults($scope.query, page || 1);
    }

    $scope.toggle = function (allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });

    db.system.enums('rateEnum').then(function (result) {
        $scope.rateTypes = result;
    });

    db.company.getSearchFolders().success(function (result) {
        $scope.folders = result;
    });

    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });
    db.system.getIndustries().success(function (result) {
        $scope.industries = result;
    });

    db.system.getFunctionals().success(function (result) {
        $scope.functionals = result;
    });

    db.company.getActiveJobs().success(function (result) {
        $scope.jobs = result;
        if (result.length > 0) {
            $scope.job = result[0];
        }
    });

    $scope.experienceTranslate = function (value) {
        if (value == 0) return 'Fresher';
        var years = Math.floor(value / 12);
        var months = value % 12;
        return years + (months > 0 ? "." + months : "") + 'y';
    }

    $scope.rateTranslate = function (value) {
        return value + 'k';
    }

    //Slider configs
    $scope.experienceSlider = {
        min: $scope.query.xfrom || 0,
        max: $scope.query.xto || 500,
        ceil: 240,
        floor: 0
    };

    $scope.rateSlider = {
        min: $scope.query.ratestart || 0,
        max: $scope.query.rateend || 500,
        ceil: 500,
        floor: 1
    };

    $scope.translate = function (value) {
        return '$' + value;
    }


    $scope.move = function (folder) {
        var i = 0;
        function moveFolder(x, folder, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
                $scope.navigate($scope.currentPage);
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.company.moveContractor($scope.records[x].id, folder).success(next);
            } else next();
        }
        function onNext() {
            moveFolder(++i, folder, onNext);
        }
        moveFolder(i, folder, onNext);
    }


    $scope.invite = function (job) {
        var i = 0;
        function inviteToJob(x, job, next) {
            if (!$scope.records[x]) {
                $scope.navigate($scope.currentPage);
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.company.inviteToJob($scope.records[x].id, job.id).success(next);
            } else next();
        }
        function onNext() {
            inviteToJob(++i, job, onNext);
        }
        inviteToJob(i, job, onNext);
    }

    $scope.resetFilters = function () {
        $scope.query.keywords = '';
        $scope.query.skills = '';
        $scope.query.location = '';
        $scope.query.startrate = '';
        $scope.query.endrate = '';
        $scope.query.xfrom = '';
        $scope.query.xto = '';
        $scope.query.industry = '';
        $scope.search(query);
    }

    $scope.$watch('selectAll', function (val) {
        $scope.toggle($scope.selectAll);
    });

    $scope.$on("slideEnded", function () {
        // user finished sliding a handle 
        $scope.query.ratestart = $scope.rateSlider.min;
        $scope.query.rateend = $scope.rateSlider.max;
        $scope.query.xfrom = $scope.experienceSlider.min;
        $scope.query.xto = $scope.experienceSlider.max;
        $scope.search($scope.query);
    });

    $scope.navigate(param.page);


}]);