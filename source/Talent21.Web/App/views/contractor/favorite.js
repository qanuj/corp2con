app.controller('contractorFavoriteController', ['$scope', 'dataService', function ($scope, db) {


    function refreshRecord() {
        return db.system.FavoriteJob().success(function (result) {
            $scope.records = result;

        });
    }

    refreshRecord();
}]);
