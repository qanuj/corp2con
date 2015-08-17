app.controller('adminTransactionController', ['$scope', 'dataService', function ($scope, db) {
    //do nothing ?
    $scope.title = "Jobs";
    $scope.navigate = function (page) {
        db.admin.getTransactions(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            angular.forEach(result.items, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
            });
            $scope.records = result.items;
        });
    }
    $scope.navigate(params.page);
}]);