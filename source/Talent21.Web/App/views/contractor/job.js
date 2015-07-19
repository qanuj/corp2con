app.controller('contractorJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {
    $scope.isCollapsed = true;
    $scope.title = "Job Profile";
    $scope.role = db.role;


    //$scope.showalertmessage = function () {
    //    $window.alert('Your application has been successfully sent.')
    //}

   
    $scope.id = param.id;
    db.contractor.jobById($scope.id).success(function (result) {
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

    $scope.apply = function (record) {
        db.contractor.ApplyToJob(record.id).success(function (result) {
            $window.history.back();
        });
    }

    $scope.favorite = function (id) {
        db.contractor.favorite(id).success(function (result) {
            $window.history.back();
        });
    }
}]);