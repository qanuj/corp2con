app.controller('contractorFavoriteController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {
    $scope.title = "Favorite Jobs";
    $scope.count= 'None';

    $scope.navigate = function (page) {
        db.contractor.getFavorite(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
            $scope.count = result.count;
            console.log(result);
        }).error(function (err) { console.log(err) });
    }

    $scope.update = function (record) {
        db.contractor.favoriteJob(record).success($scope.navigate(params.page));
    };

    $scope.delete = function (record) {
        db.contractor.deleteFavoriteJob(record).success($scope.navigate(params.page));
    };

    $scope.toggle = function (record) {
        record.editMode = !record.editMode;
    };

    $scope.navigate();

}]);

