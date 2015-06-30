app.controller('companyProfileController', ['$scope', 'dataService', function ($scope, db) {
    db.company.profile().success(function (result) {
        $scope.record = result;
    });
}]);