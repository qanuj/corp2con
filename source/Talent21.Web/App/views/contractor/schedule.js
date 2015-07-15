app.controller('jobscheduleController', ['$scope', 'dataService', '$window', function ($scope, db, window) {

    $scope.isCollapsed = false;
    $scope.save = function (record) {
        console.log(record)
        db.contractor.createSchedule(record).success(function (result) {
            console.log(result);
        });
    }
    db.contractor.getSchedule().success(function (result) {
        console.log(result)
        angular.forEach(result, function(d) {
            d.start = new Date(d.start);
            d.end = new Date(d.end);
        });
        $scope.schedule = result;
    });

      $scope.update = function (s) {
        db.contractor.editSchedule(s).success(function (result) {
            console.log(result);
        });
    };

      $scope.delete = function (s) {
          console.log(s);
          db.contractor.deleteSchedule(s).success(function (result) {
          });
      };

    //  Directives.directive('ngConfirmClick', [
    //function () {
    //    return {
    //        priority: -1,
    //        restrict: 'A',
    //        link: function (scope, element, attrs) {
    //            element.bind('click', function (e) {
    //                var message = attrs.ngConfirmClick;
    //                if (message && !confirm(message)) {
    //                    e.stopImmediatePropagation();
    //                    e.preventDefault();
    //                }
    //            });
    //        }
    //    }
    //}
    //  ]);
   
    //Date picker start
    $scope.datePicker = (function () {
        var method = {};
        method.instances = [];

        method.open = function ($event, instance) {
            $event.preventDefault();
            $event.stopPropagation();

            method.instances[instance] = true;
        };

        method.options = {
            'show-weeks': false,
            startingDay: 0
        };

        var formats = ['MM/dd/yyyy', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        method.format = formats[0];

        return method;
    }());
    //Date picker End
}]);