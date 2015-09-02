app.controller('companyBillingController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Billing Transactions";
    $scope.navigate = function (page) {
        db.company.transactions(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }
    $scope.navigate($routeParams.page);
}]);

