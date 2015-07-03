app.controller('jobProfileController', ['$scope', 'dataService','$routeParams', function ($scope, db, param) {
    $scope.title = "Job Profile";
    $scope.role = db.role;
    alert($scope.role)
    $scope.id = param.id;
    db.job.profile($scope.id).success(function (result) {
        $scope.record = result;
        console.log($scope.record);
    });
}]);