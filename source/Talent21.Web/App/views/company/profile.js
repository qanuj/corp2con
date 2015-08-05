app.controller('companyProfileController', ['$scope', 'dataService', '$routeParams', function ($scope, db, param) {

    $scope.title = "Company Profile";

    $scope.role = db.role;
    $scope.page = 1;
    $scope.pages = 1;

    db.company.get(param.id).success(function (result) {
        $scope.record = result;
        $scope.page = db.currentPage;
    });

    function refreshJobs()
    {
        db.job.paged($scope.page).success(function (result) {
            $scope.jobs = result.items;
            console.log(result);
        });
    }

    refreshJobs();

}]);