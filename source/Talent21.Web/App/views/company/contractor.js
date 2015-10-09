app.controller('companyContractorController', ['$scope', 'dataService', '$stateParams', '$rootScope', 'toastr', function ($scope, db, param, $rootScope, toastr) {
        $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.shortlist=function(id) {
        
    }

    $scope.folders = [
        "Shortlisted",
        "Rejected"
    ];

    $scope.updateRate=function(record) {
        db.company.updateBenchRate({
            id: record.id,
            rateType: record.rateType,
            rate:record.rate
        }).success(function (result) {
            if (result == true) {
                toastr.success('Success', 'New Rates Updates. Enjoy!');
            }
        }).error(function (err) {
            toastr.error(err.exceptionMessage, err.message);
        });
    }

    db.system.enums('proficiencyEnum').then(function (enums) {
        $scope.proficiencies = enums;
        db.system.enums('levelEnum').then(function (levels) {
            $scope.levels = levels;
        });
        db.company.getContractor(param.id).success(function (result) {
            $scope.record = result;
            db.company.visitContractor(param.id);
        });
    });

    $scope.move = function (id, folder) {
        db.company.moveContractor(id, folder).success(function () {
            console.log('moved');
        });
    }

    $scope.download = function (id, folder) {
        db.company.moveContractor(id, folder).success(function () {
            console.log('moved');
        });
    }

}]);