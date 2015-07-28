app.controller('contractorJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {
    $scope.isCollapsed = true;
    $scope.title = "Job Profile";
    $scope.role = db.role;
    $scope.id = param.id;

    $scope.checkIfApplied = function () {
        var getJobStatus = function (jobId, appliedJobs) {
            $scope.jobApplied = appliedJobs.indexOf(jobId) > -1;
            return $scope.jobApplied;
        };
        if (db.applied.length < 1) {
            $scope.getApplications = function () {
                db.contractor.getJobApplications().success(function (result) {
                    angular.forEach(result.items, function (item) {
                        db.applied.push(item.job.id);
                    });
                    getJobStatus(param.id,db.applied);
                });
            }
            $scope.getApplications();
        }
        else {
            getJobStatus(param.id,db.applied);
        }
    };

    $scope.checkIfApplied();

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