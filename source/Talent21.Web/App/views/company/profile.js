app.controller('companyProfileController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Company Profile";
    db.company.profile().success(function (result) {
        $scope.record = result;
        $scope.page = db.currentPage;
        console.log($scope.record);
    });
}]);