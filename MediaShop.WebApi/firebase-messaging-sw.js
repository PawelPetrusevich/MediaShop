importScripts('https://www.gstatic.com/firebasejs/3.6.8/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/3.6.8/firebase-messaging.js');

firebase.initializeApp({
    messagingSenderId: 'AAAAABLD-38:APA91bHIz5MVWPxYLRds5AkgAb_Rw0rGBlIvZ643-KDSz1GXF3PxcqULLMtoj1A8QBPSwa5QN1b7u6kBv3Tat1jL4oCeJFtvTR7A0lLxbMNuVdd4-DTsUS0-Xqkk6lFsorGduqvjLsPW'
});

const messaging = firebase.messaging();