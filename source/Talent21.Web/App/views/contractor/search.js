app.controller('contractorSearchController', ['$scope', 'dataService', '$rootScope', '$state', function ($scope, db, $rootScope, $state) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Jobs : Search Result";
    var param = db.args();

    if (!isNaN(param.idea)) {
        param.page = param.idea;
    } else if (param.idea == "match") {
        $scope.title = "Matching Jobs for you.";
    } else if (param.idea == "month") {
        $scope.title = "Matching Jobs for you, next month";
    } else if (param.idea == "week") {
        $scope.title = "Matching Jobs for you, next week";
    }

    console.log('Searching Job', param, param);

    $scope.favoriteAll = function (record) {
        var i = 0;
        function favoriteJob(x, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
                $scope.navigate($scope.currentPage);
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.contractor.favorite($scope.records[x].id).success(next);
            } else next();
        }
        function onNext() {
            favoriteJob(++i, onNext);
        }
        favoriteJob(i, onNext);
    }

    $scope.applyAll = function () {
        var i = 0;
        function applToJobFolder(x, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
                $scope.navigate($scope.currentPage);
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.contractor.applyToJob($scope.records[x].id).success(next);
            } else next();
        }
        function onNext() {
            applToJobFolder(++i, onNext);
        }
        applToJobFolder(i, onNext);
    }

    $scope.query = {
        keywords: param.q || param.keywords || '',
        location: param.location || '',
        skills: param.skills || '',
        startrate: param.startrate || '',
        endrate: param.endrate || '',
        xfrom: param.xfrom || '',
        xto: param.xto || '',
        //industry: param.industry || ''
    }

    function fetchResults(query, page) {
        db.contractor.search(query, page).then(function (result) {
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
    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });
    db.system.getIndustries().success(function (result) {
        $scope.industries = result;
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
        ceil: 500,
        floor: 0
    };

    $scope.rateSlider = {
        min: $scope.query.startrate || 0,
        max: $scope.query.endrate || 500,
        ceil: 500,
        floor: 0
    };

    $scope.translate = function (value) {
        return 'INR' + value;
    }

    $scope.move = function (folder) {
        var i = 0;
        function moveFolder(x, folder, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
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
        console.log('Toggeling', val);
    });


    $scope.$on("slideEnded", function () {
        // user finished sliding a handle 
        $scope.query.startrate = $scope.rateSlider.min;
        $scope.query.endrate = $scope.rateSlider.max;
        $scope.query.xfrom = $scope.experienceSlider.min;
        $scope.query.xto = $scope.experienceSlider.max;
        $scope.search($scope.query);
    });

    $scope.navigate(param.page);

}]);