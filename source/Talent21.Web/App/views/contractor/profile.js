app.controller('contractorProfileController', ['$scope', 'dataService', function ($scope, db) {
    db.contractor.profile().success(function (result) {
        $scope.record = result;
    });
}]);