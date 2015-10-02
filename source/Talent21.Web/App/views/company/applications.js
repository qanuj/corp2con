app.controller('companyApplicationsController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, $stateParams, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Job Applications";
    $scope.save = "Save";

    var id = $stateParams.id;
    $scope.id = id;
    $scope.navigate = function (page) {
        $scope.query = {
            folder: $stateParams.folder || '',
            id:id
        }
        function fetchResults(query, page) {
            db.company.getJobApplications($scope.query, page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.count = result.count;
                $scope.records = result.items;
                $scope.selectAll = false;
                if (result.items.length > 0) {
                    $scope.jobId = $scope.records[0].job.id;
                }
            });
        }

        db.company.getFolders($stateParams.id).success(function (result) {
            $scope.folders = result;
        });

        fetchResults($scope.query, page || 1);
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
                db.company.saveApplication($scope.records[x].appicationId, folder).success(next);
            } else next();
        }
        function onNext() {
            moveFolder(++i, folder, onNext);
        }
        moveFolder(i, folder, onNext);
    }

    $scope.toggle = function (allSelected) {
       console.log(allSelected);
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }
    $scope.$watch('selectAll', function (val) {
        $scope.toggle($scope.selectAll);
    });

    $scope.navigate($stateParams.page);

}]);
