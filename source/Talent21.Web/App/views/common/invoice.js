app.controller('invoiceController', ['$scope', 'dataService', '$stateParams', function ($scope, db, $stateParams) {
    $scope.title = "Invoice";
    $scope.noCreditMessage = "Start Adding more credits and Promote your Profile to leading companies around world.";
    $scope.noCreditMessage = "Start Adding more credits and Promote your Profile to leading companies around world.";
    $scope.firstCredit = 10;
    $scope.addMessage = "Add 10 Credits to Start Promoting Your Profile";

    $scope.navigate = function (id) {
        db.billing.transaction(id).success(function (result) {
            $scope.record = result;
        });
    }
    $scope.navigate($stateParams.id);
}]);

