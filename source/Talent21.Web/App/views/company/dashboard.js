app.controller('companyDashboardController', ['$scope','dataService', function ($scope,db) {
    db.company.dashboard().success(function (result) {
        $scope.record = result;
    });

    db.company.get().success(function (result) {
        $scope.profile = result;
    });
}]);