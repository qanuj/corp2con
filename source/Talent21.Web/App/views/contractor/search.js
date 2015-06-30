app.controller('contractorSearchController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Companies : Search Result";
    db.contractor.paged().success(function (result) {
        $scope.records = result;
    });
}]);