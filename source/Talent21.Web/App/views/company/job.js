app.controller('companyJobController', ['$scope', 'dataService', '$stateParams', '$window', '$rootScope', function ($scope, db, param, $window, $rootScope) {
   
    $scope.title = "Job Profile";
    $scope.role = db.role;
    $scope.id = param.id;

    db.job.get($scope.id).success(function (result) {
        $scope.record = result;
        console.log($scope.record);
    });

    $scope.publish = function (id) {
        db.job.publish(id).success(function (result) {
            $window.location.href = '/#/jobs';
        });
    }

    $scope.unpublish = function (record) {
        db.contractor.unpublish(id).success(function (result) {
            $scope.record.ispublish = false;
        });
    }
    $scope.cancel = function (id) {
        db.job.cancel(id).success(function (result) {
            $window.location.href = '/#/jobs';
        });
    }

    $scope.delete = function (id) {
        db.job.delete(id).success(function (result) {
            $window.location.href = '/#/jobs';
        });
    }
    $scope.view = function (id) {
        db.job.view(id).success(function (result) {
            $window.location.href = '/#/SingleJobApplications';
        });
    }
}]);