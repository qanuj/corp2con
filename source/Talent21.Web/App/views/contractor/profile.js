app.controller('contractorProfileController', ['$scope', 'dataService', function ($scope, db) {

    $scope.currentPage = db.currentPage;

    db.contractor.profile().success(function (result) {
        $scope.record = result;
    });
}]);