app.controller('companyApplicationsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Job Applications";
    var id = $routeParams.id;
    $scope.id = id;
    $scope.navigate = function (page) {
        $scope.query = {
            folder: $routeParams.folder || ''
        }
        function fetchResults(query, page) {
            db.company.getJobApplications(id, page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.records = result.items;
                $scope.jobId = $scope.records[0].job.id;
            });
        }

        db.company.getFolders($routeParams.id).success(function (result) {
            $scope.folders = result;
        });

        fetchResults($scope.query, page || 1);
    }
    $scope.navigate($routeParams.page);

}]);
