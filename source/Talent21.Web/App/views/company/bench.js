app.controller('companyBenchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Bench Management";
    $scope.save = "Save";

    if (!isNaN($routeParams.idea)) {
        param.page = page.idea;
    } else if ($routeParams.idea == "match") {
        $scope.searching = "Matching Jobs for you.";
    } else if ($routeParams.idea == "month") {
        $scope.searching = "Matching Jobs for you, next month";
    } else if ($routeParams.idea == "week") {
        $scope.searching = "Matching Jobs for you, next week";
    }

    $scope.navigate = function (page) {
        $scope.query = {
            keywords: $routeParams.q || $routeParams.keywords || '',
            location: $routeParams.location || '',
            folder: $routeParams.folder || '',
            industry: $routeParams.industry || '',
            functional: $routeParams.functional || '',
            skills: $routeParams.skills || '',
            ratestart: $routeParams.ratestart || '',
            rateend: $routeParams.rateend || '',
            xfrom: $routeParams.xfrom || '',
            xto: $routeParams.xto || '',
            ratetype: $routeParams.ratetype || ''
        }

        function fetchResults(query, page) {
            db.company.bench(query, page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.count = result.count;
                $scope.records = result.items;
                $scope.page = page;
            });
        }

        $scope.search = function (query) {
            var q = '';
            for (var x in query) {
                q += (q === '' ? '?' : '&') + x + '=' + query[x];
            }
            window.location = '#/bench' + q;
        }

        fetchResults($scope.query, page || 1);
    }
    $scope.navigate($routeParams.page);

    $scope.toggle=function(allSelected) {
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

    db.company.getBenchFolders().success(function (result) {
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

    $scope.experienceTranslate = function (value) {
        if (value == 0) return 'Fresher';
        var years = Math.floor(value / 12);
        var months = value % 12;
        return years + (months > 0 ? "." + months : "") + 'y';
    }

    $scope.rateTranslate = function (value) {
        return value+'k';
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
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.company.moveContractor($scope.records[x].id, folder).success(next);
            }else next();
        }
        function onNext() {
            moveFolder(++i, folder, onNext);
        }
        moveFolder(i, folder, onNext);
    }

    $scope.$on("slideEnded", function () {
        // user finished sliding a handle 
        console.log('slide ended');
        $scope.query.ratestart = $scope.rateSlider.min;
        $scope.query.rateend = $scope.rateSlider.max;
        $scope.query.xfrom = $scope.experienceSlider.min;
        $scope.query.xto = $scope.experienceSlider.max;
        $scope.search($scope.query);
    });


}]);