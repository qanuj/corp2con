app.controller('contractorSearchController', ['$scope', 'dataService', '$rootScope', '$state', '$stateParams', function ($scope, db, $rootScope, $state, $stateParams) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Jobs : Search Result";
    $scope.query = $stateParams;

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

    function fetchResults(query, page) {
        db.contractor.search(query, page).then(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
            $scope.page = page;
            $scope.start = ((page - 1) * db.pageSize) + 1;
            $scope.end = $scope.start + result.items.length - 1;
            $scope.selectAll = false;
        });
    }

    $scope.search = function (query) {
        $state.go('search', query, { location: true, reload: true });
    }

    $scope.navigate = function (page) {
        fetchResults($scope.query, page || 1);
    }

    $scope.toggle = function (allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

    $scope.experienceTranslate = function (value) {
        if (value == 0) return 'Fresher';
        var years = Math.floor(value / 12);
        var months = value % 12;
        return years + (months > 0 ? "." + months : "") + 'y';
    }

    $scope.rateTranslate = function (value) {
        return value + 'k';
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

    $scope.$watch('selectAll', function (val) {
        $scope.toggle($scope.selectAll);
    });

    $scope.toggle = function (allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

    $scope.addRemoveFilter = function (name, value, selected) {
        $scope[selected ? 'addFilter' : 'removeFilter'](name, value);
    }

    $scope.addFilter = function (name, value) {
        console.log(name);
        if ((name == 'skills') && $scope.query[name]) {
            $scope.query[name] += ",";
        } else {
            $scope.query[name] = "";
        }
        $scope.query[name] += value;
        $scope.search($scope.query);
    }

    $scope.removeFilter = function (name, value) {
        if (!value || $scope.query[name]==value) {
            $scope.query[name] = null;
        }
        else if (name == 'skills') {
            $scope.query[name] = $scope.query[name].replace(',' + value);
        } else {
            $scope.query[name] = null;
        }
        $scope.search($scope.query);
    }

    db.job.filters($scope.query).success(function (result) {
        $scope.filters = result;
    });

    fetchResults($scope.query, 1);

}]);