app.controller('contractorSearchController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Companies : Search Result";
    db.company.paged().success(function (result) {
        $scope.records = result;
    });
}]);