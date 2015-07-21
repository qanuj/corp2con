app.controller('contractorProfileController', ['$scope', 'dataService', '$routeParams', function ($scope, db, param) {
    db.contractor.get(param.id).success(function (result) {
        $scope.record = result;
    });
}]);