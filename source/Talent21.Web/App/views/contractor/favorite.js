app.controller('contractorFavoriteController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {

    $scope.navigate = function (page) {

        db.contractor.getFavorite(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
            console.log(result);
        });


        $scope.update = function (record) {
            db.contractor.favoriteJob(record).success($scope.navigate(params.page));
        };

        $scope.delete = function (record) {
            db.contractor.deleteFavoriteJob(record).success($scope.navigate(params.page));
        };

        $scope.toggle = function (record) {
            record.editMode = !record.editMode;
        };

        function refreshRecord() {
            return db.contractor.getFavorite().success(function (result) {
                $scope.records = result;
            });
        }
    }
    $scope.navigate(params.page);
}]);

