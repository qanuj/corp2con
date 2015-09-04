app.controller('companyBillingController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, $stateParams, $rootScope) {
    
    $scope.title = "Billing Transactions";
    $scope.navigate = function (page) {
        db.company.balance(page).success(function (result) {
            $scope.balance = result;
        });
        db.company.transactions(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }
    
    $scope.addCredits = function (credits) {
        if (credits <= 0) return;
        db.company.addCredits(credits).success(function (result) {
            if (result.isError) {
                $scope.error = result.error;
                return;
            }
            window.location = result.url;
        });
    }
    $scope.navigate($stateParams.page);
}]);

