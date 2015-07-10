app.controller('contractorJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {

    $scope.title = "Job Profile";
    $scope.role = db.role;

    $scope.id = param.id;
    db.job.profile($scope.id).success(function (result) {
        $scope.record = result;
        console.log($scope.record);
    });

    $scope.publish = function (record) {
        db.job.publish(id).success(function (result) {
            console.log('Job Published');
        });
    }

    $scope.cancel = function (record) {
        db.job.cancel(record).success(function (result) {
            console.log('Job Cancelled');
        });
    }

    $scope.delete = function (record) {
        db.job.delete(record).success(function (result) {
            $window.location.href = '/#/myjobs';
        });
    }
}]);