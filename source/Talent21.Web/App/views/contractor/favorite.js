app.controller('contractorFavoriteController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Favorite Jobs";
    $scope.getFavoriteApplications = function () {
        db.contractor.getFavoriteApplications().success(function (result) {
            $scope.records = result;
            console.log($scope.records);
        });
    }
    $scope.getFavoriteApplications();
}]);