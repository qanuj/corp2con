app.controller('companyProfileController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Company Profile";

    $scope.currentPage = db.currentPage;

    db.company.get().success(function (result) {
        $scope.record = result;
        $scope.page = db.currentPage;
    });

    function refreshJobs()
    {
        db.job.paged($scope.page||1).success(function (result) {
            $scope.jobs = result.items;
            $scope.page = db.currentPage;
            console.log($scope.jobs);
        });

    }

    refreshJobs();

}]);