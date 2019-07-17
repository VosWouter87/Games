// START IMPORTS
// import does not work in Codepen, using const instead
const {Component} = ng.core;
const {bootstrap} = ng.platform.browser;
// END IMPORTS

// START Components
// Please note: as we are not exporting/importing but everything is in a single file, we need to:
// inline the templates as strings, and
// list the components so that any subcomponents come before the component using them (i.e. app is last).

// --- MyForm
@Component({
    selector: 'my-form',
    template: `
<section>
  <p>{{title}}</p>
  <button (click)="changeTitle('koe')">
    CHANGE
  </button>
</section>
    `
})
class MyForm {
  constructor(){
    this.title = "hello world"
  }
  
  changeTitle(newTitle){
    this.title = newTitle + " " + new Date();
  }
}
// --- MyForm

// --- MyApp
@Component({
    selector: 'my-app',
    directives: [MyForm],
    template: `
<section>
  <h1>Angular 2 Beta</h1>
  <my-form></my-form>
  <my-form></my-form>
</section>
    `
})
class AppComponent {
  constructor(){
    console.log("app started");
  }
}
// --- MyApp

// END Components



// BOOT ANGULAR:
bootstrap(AppComponent);
