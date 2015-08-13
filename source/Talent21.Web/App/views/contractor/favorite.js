app.controller('contractorFavoriteController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {

        db.contractor.getFavorite().success(function (result) {
            console.log('hello', result);
            $scope.records = result;
            
        }).error(function (err) { console.log(err) });


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
}]);

