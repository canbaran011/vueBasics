var app = new Vue({
    el:'#app',
    data:{
        message:'hello VueJs'
    }
})

var app2= new Vue({
    el:"#app-2",
    data:{
        message: 'You loaded this page on ' + new Date().toLocaleString()  
      }
})

var app3 = new Vue({
    el: "#app-3",
    data:{
        seen: true
    }
})

var app4 = new Vue({
    el:"#app-4",
    data:{
        todos : [
            { text : "learn javascript "},
            { text : "learn Vuejs "},
            { text : "learn NodeJs "}
// on console addin new item app4.todos.push({text :"new item"})
        ]
    }
})

var app5 = new Vue({
    el:"#app-5",
    data:{
        message : "hello vueJS"
    },
    methods:{
        reverseMessage: function() {
            this.message = this.message.split('').reverse().join('')
        }
    }
})

var app6 = new Vue({
    el:"#app-6",
    data:{
        message:" Hoey VUEJS"
    }
})

/************************ */

// Vue.component("todo-item" , {
//     template: <li> This is simpleTodo item </li>
// })

// var app = new Vue(...)

/************************ */

// Vue.component('todo-item',{
//     // todo item compoent now accepts a prop , which is  like a custom attrb. 
//     // this prop is called todo
//     props:['todo'],
//     template: "<li>{{ todo.text}}</li>"
// })

// Vue.component('todo-item', {
//     // The todo-item component now accepts a
//     // "prop", which is like a custom attribute.
//     // This prop is called todo.
//     props: ['todo'],
//     template: '<li>{{ todo.text }}</li>'
//   })

Vue.component( "todo-item" , {
    props : ['todo'],
    template: '<li>{{ todo.text }} </li>'
})
var app7 = new Vue({
    el:"#app-7",
    data:{
        groceryList:[
            { id:0 ,text: 'Vegatables'},
            { id:1 ,text: 'Fruits'},
            { id:2 ,text: 'Books'},
        ]
    }
})




