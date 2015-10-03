app.controller('companySearchController', ['$scope', 'dataService', '$rootScope', '$stateParams', '$state', function ($scope, db, $rootScope, $stateParams, $state) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.loadSkills = db.system.getSearchSkills;
    $scope.title = "Contractor : Search Result";
    $scope.save = "Save";

    $scope.query = $stateParams;

    function fetchResults(query, page) {

        db.company.getSearchFolders().success(function (result) {
            $scope.folders = result;
        });

        db.company.getActiveJobs().success(function (result) {
            $scope.jobs = result;
            if (result.length > 0) {
                $scope.job = result[0];
            }
        });

        db.company.filters(query).success(function (result) {
            $scope.filters = result;
        });
        db.company.search(query, page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
            $scope.page = page;
            $scope.selectAll = false;
        });
    }

    $scope.addFilter = function (name, value) {
        $scope.query[name] = value;
        $scope.search($scope.query);
    }

    $scope.removeFilter = function (name) {
        $scope.query[name] = null;
        $scope.search($scope.query);
    }

    $scope.hasFilter = function (name) {
        return !!$scope.query[name];
    }

    $scope.search = function (query) {
        console.log($scope.query);
        $state.transitionTo('search', query, { location:true,reload:false });
    }

    $scope.navigate = function (page) {
        fetchResults($scope.query, page || 1);
    }

    $scope.toggle = function (allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

    $scope.translate = function (key) {
        return $scope[key + 'Translate'];
    }

    $scope.experienceTranslate = function (value) {
        if (value == 0) return 'Fresher';
        var years = Math.floor(value / 12);
        var months = value % 12;
        return years + (months > 0 ? "." + months : "") + 'y';
    }

    $scope.ratesTranslate = function (value) {
        return value;
    }

    $scope.move = function (folder) {
        var i = 0;
        function moveFolder(x, folder, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
                db.company.getSearchFolders().success(function (result) {
                    $scope.folders = result;
                });
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

    fetchResults($scope.query,1);

}]);