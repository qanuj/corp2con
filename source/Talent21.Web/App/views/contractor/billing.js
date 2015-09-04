app.controller('contractorBillingController', ['$scope', 'dataService', '$stateParams', function ($scope, db, $stateParams) {
    $scope.title = "Billing Transactions";
    $scope.navigate = function (page) {
        db.contractor.balance(page).success(function (result) {
            $scope.balance = result;
        });
        db.contractor.transactions(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }
    
    $scope.addCredits = function (credits) {
        if (credits <= 0) return;
        db.contractor.addCredits(credits).success(function (result) {
            if (result.isError) {
                $scope.error = result.error;
                return;
            }
            window.location = result.url;
        });
    }
    $scope.navigate($stateParams.page);
}]);

