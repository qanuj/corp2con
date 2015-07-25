app.controller('companyDashboardController', ['$scope','dataService', function ($scope,db) {
    db.company.dashboard().success(function (result) {
        $scope.record = result;
    });

    db.company.get().success(function (result) {
        $scope.profile = result;
    });

    db.company.getTopProfiles('aspnet', 'mumbai', 1, 5).success(function (result) {
        $scope.topRecords = result;
    });

    db.company.getLatestProfiles('aspnet', 'mumbai', 1, 5).success(function (result) {
        $scope.latestRecords = result;
    });

}]);