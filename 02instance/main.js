
// creating an instance
// vue inspired by MVVM patern

// var vm =new Vue({
//      //options
// })


// Root Instance
// └─ TodoList
//    ├─ TodoItem
//    │  ├─ DeleteTodoButton
//    │  └─ EditTodoButton
//    └─ TodoListFooter
//       ├─ ClearTodosButton
//       └─ TodoListStatistics

// our data object
var data = { a: 1}

//the object is added o a VUE instance

var vm = new Vue({
    data:data
})

// getting property from on the instace
//returns the one from the origina data

vm.a == data.a// => true

//setting the property on the instance 
//also affects the original data
vm.a = 2
data.a // => 2

//and vice versa

data.a =3
vm.a // 3


// data: {
//     newTodoText: '',
//     visitCount: 0,
//     hideCompletedTodos: false,
//     todos: [],
//     error: null
//   }

// INSTANCE LIFECYCLE HOOKS
// created , mounted , updated ,destroyed

new Vue({
    data :{
        a: 1
    },
    created: function() {
        // this points to the vm instance
        console.log("a is :" + this.a);
    },
}) //a is : 1

// Don’t use arrow functions on an options property or callback, such as created: () => console.log(this.a) or vm.$watch('a', newValue => this.myMethod()). Since an arrow function doesn’t have a this, this will be treated as any other variable and lexically looked up through parent scopes until found, often resulting in errors such as Uncaught TypeError: Cannot read property of undefined or Uncaught TypeError: this.myMethod is not a function.


