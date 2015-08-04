app.controller('contractorProfileController', ['$scope', 'dataService', '$routeParams', function ($scope, db, param) {
    $scope.role = db.role;
    db.contractor.get(param.id).success(function (result) {
        console.log('Contractor profile',param);
        $scope.record = result;
    });
}]);