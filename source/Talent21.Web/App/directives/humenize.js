'use strict';
angular
  .module('humenize', [])
 .filter('uncamel', function () {
     function decamelize(str, sep) {
         if (typeof str !== 'string') {
             throw new TypeError('Expected a string');
         }
         return str.replace(/([a-z\d])([A-Z])/g, '$1' + (sep || '_') + '$2').toLowerCase();
     }
     return function (input, allLower) {

         if (typeof input !== "string") {
             return input;
         }

         var result = decamelize(input, ' ');

         if (!allLower) {
             result = result.charAt(0).toUpperCase() + result.slice(1);
         }

         return result;
     };
 })
 .filter('readable', function () {
     return function (input, mode) {

         if (input == true) return "Yes";
         if (input == false) return "No";
         if (input == null) return "Uspecified";
         if (mode == 'date') return moment(input).fromNow();
         if (mode == 'inr') return ">= ₹" + input.toFixed(0).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');

         return input;

     };
 })
 .filter('fromNow', function () {
     return function (date) {
         return moment(date).fromNow();
     }
 })
 .filter('amHumanize', function () {
     return function (date,to) {
         return moment.duration(moment(date).diff(to)).humanize();
     }
 })
 .filter('plaintext', function () {
     return function (html) {
         var div = document.createElement("div");
         div.innerHTML = html;
         return div.textContent || div.innerText || "";
     };
 })
 .filter('years', function () {
     return function (value, inbox) {
         if (isNaN(value)) return value;
         if (value == 0) return '';
         var years = Math.floor(value / 12);
         var months = value % 12;
         var val = years + (months > 0 ? "." + months : "") + 'y';
         if (inbox) return "(" + val + ")";
         return val;
     };
 });