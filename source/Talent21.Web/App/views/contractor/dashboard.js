app.controller('contractorDashboardController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.dashboard().success(function (result) {
        $scope.record = result;
    });

    db.contractor.get().success(function (result) {
        $scope.profile = result;
    });

}]);