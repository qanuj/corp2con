app.controller('contractorSearchController', ['$scope', 'dataService', '$rootScope', '$stateParams', function ($scope, db,$rootScope, $stateParams) {
   
    $scope.title = "Jobs : Search Result";
    var param = $stateParams;

    if (!isNaN($stateParams.idea)) {
        param.page = $stateParams.idea;
    } else if ($stateParams.idea == "match") {
        $scope.title = "Matching Jobs for you.";
    } else if ($stateParams.idea == "month") {
        $scope.title = "Matching Jobs for you, next month";
    } else if ($stateParams.idea == "week") {
        $scope.title = "Matching Jobs for you, next week";
    }

    $scope.$watch('selectAll', function () {
        $scope.toggle($scope.selectAll);
    });

    $scope.toggle=function(allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

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
        function applToJobFolder(x,next) {
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
            applToJobFolder(++i,onNext);
        }
        applToJobFolder(i,onNext);
    }

    $scope.navigate = function (page) {
        $scope.query = {
            keywords: $stateParams.q || $stateParams.keywords || '',
            location: $stateParams.location || '',
            skills: $stateParams.skills || '',
            startrate: $stateParams.startrate || '',
            endrate: $stateParams.endrate || '',
            xfrom: $stateParams.xfrom || '',
            xto: $stateParams.xto || '',
            industry: $stateParams.industry || ''
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
            window.location = '#/search' + q;
        }

        fetchResults($scope.query, page || 1);
    }

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    }).then(function () {
        return db.system.getLocations().success(function (result) {
            $scope.locations = result;
        });
    }).then(function() {
        return db.system.getIndustries().success(function (result) {
            $scope.industries = result;
        });
    }).then(function () {
        return db.system.getLocations().success(function (result) {
            $scope.locations = result;
        });
    }).then(function () {
        $scope.navigate($stateParams.page);
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
                } else next();
            }
            function onNext() {
                moveFolder(++i, folder, onNext);
            }
            moveFolder(i, folder, onNext);
        }


        $scope.$on("slideEnded", function () {
            // user finished sliding a handle 
            $scope.query.startrate = $scope.rateSlider.min;
            $scope.query.endrate = $scope.rateSlider.max;
            $scope.query.xfrom = $scope.experienceSlider.min;
            $scope.query.xto = $scope.experienceSlider.max;
            $scope.search($scope.query);
        });
    });
   
}]);