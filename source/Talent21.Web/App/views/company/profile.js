app.controller('companyProfileController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Company Profile";

    $scope.page = 1;
    $scope.pages = 1;

    db.company.get().success(function (result) {
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