app.controller('contractorDashboardController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {

    db.contractor.dashboard().success(function (result) {
        $scope.record = result;

    });

    db.contractor.get().success(function (result) {
        $scope.profile = result;
    });
}]);

