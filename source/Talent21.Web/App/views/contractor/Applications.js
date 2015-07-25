app.controller('contractorApplicationsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Job Applications";

    $scope.getApplications = function () {
        db.contractor.getJobApplications().success(function (result) {
            console.log(result);
            $scope.records = result;
        });
    }

    $scope.getApplications();

}]);