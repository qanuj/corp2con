app.controller('companyApplicationsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Job Applications";
    $scope.navigate = function (page) {
        var id = $routeParams.id;
        db.contractor.getJobApplications(id, page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }
    $scope.navigate($routeParams.page);
}]);
