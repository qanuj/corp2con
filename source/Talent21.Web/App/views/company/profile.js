app.controller('companyProfileController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Company Profile";

    $scope.currentPage = db.currentPage;

    db.company.profile().success(function (result) {
        $scope.record = result;
        $scope.page = db.currentPage;
    });

    db.company.limitJobs().success(function (result) {
        $scope.jobs = result.items;
        $scope.page = db.currentPage;
        console.log($scope.jobs);
    });

}]);