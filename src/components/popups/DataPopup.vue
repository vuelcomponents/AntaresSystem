
<template>
    <main class="data-popup">
        <section>
            <MenuBar class="on-dark-bg flex-left noradius" :model="menu" style="padding:30px;"/>

            <v-stepper
                    :items="stepperSettings.items"
                    v-model="this.stepper"
                    class="no-bg-stepper"
                    :editable="true"
                    @update:modelValue="console.log('dasd')"
                    style="margin-bottom: 20px; padding: 0; "
            >

                <template v-slot:[`item.1`] v-if="this.stepperSettings.items.length>0">
                    <slot name="underMenu"></slot>
                    <div class="form-inner-content">
                        <div  class="card"
                              :style="`${this.stepperSettings.items[0].grid ?  'display: grid;' : 'display:flex'}
                                      ${this.stepperSettings.items[0].template ? 'grid-template-columns: ' + this.stepperSettings.items[0].template : ''}`"
                        >
                            <slot name="item1contentLeft" />
                            <slot name="item1contentRight" />
                        </div>
                    </div>
                </template>
                <template v-slot:[`item.2`] v-if="this.stepperSettings.items.length>1">
                    <div class="form-inner-content">
                        <div :style="`${this.stepperSettings.items[1].grid ?  'display: grid;' : 'display:flex'}
                                      ${this.stepperSettings.items[1].template ? 'grid-template-columns: ' + this.stepperSettings.items[1].template : ''}`"
                        >
                            <slot name="item2contentLeft" />
                            <slot name="item2contentRight" />
                        </div>
                    </div>
                </template>
                <template v-slot:[`item.3`] v-if="this.stepperSettings.items.length>2">
                    <div class="form-inner-content">
                        <div :style="`${this.stepperSettings.items[2].grid ?  'display: grid;' : 'display:flex'}
                                      ${this.stepperSettings.items[2].template ? 'grid-template-columns: ' + this.stepperSettings.items[2].template : ''}`"
                        >
                            <slot name="item3contentLeft" />
                            <slot name="item3contentRight" />
                        </div>
                    </div>
                </template>
                <template v-slot:[`item.4`] v-if="this.stepperSettings.items.length>3">
                    <div class="form-inner-content">
                        <div :style="`${this.stepperSettings.items[0].grid ?  'display: grid;' : 'display:flex'}
                                      ${this.stepperSettings.items[0].template ? 'grid-template-columns: ' + this.stepperSettings.items[0].template : ''}`"
                        >
                            <slot name="item4contentLeft" />
                            <slot name="item4contentRight" />
                        </div>
                    </div>
                </template>
            </v-stepper>


        </section>
    </main>
</template>

<script>
import 'vuetify/dist/vuetify.min.css';
import { VStepper }from "vuetify/labs/components";
export default {
    name: "DataPopup",
  components:{VStepper},
    props:{
        menuItems:{
            required:true
        },
        stepperSettings:{
            required:true
        },
    },
    mounted(){
      if(this.menuItems){
          this.menu = this.menuItems
      }
    },
    data(){
        return{
            menu:[],
            stepper:undefined
        }
    }
}
</script>

<style>
.v-stepper-item--selected .v-stepper-item__avatar.v-avatar, .v-stepper-item__avatar.v-avatar {
  display:none!important;
}
.data-popup{

    section{
        background: rgba(252, 251, 251, 0.97);

    }
}
.form-inner-content{
 width:100%;
  overflow-y:auto;
  box-sizing:border-box;
}
.data-popup-cloud{
    padding: 5px;
   .data-popup-cloud-item{
       background:#fff;
       width:100%;
       height:100px;
       border-radius:3px;
       box-shadow:3px 3px 3px #00000010;
       padding:3px;
       color:#00000090!important;
   }
    .data-popup-cloud-item *{
        color: rgba(0, 0, 0, 0.85) !important;
    }
}
.no-bg-stepper{
    background:none!important;
  width:100%!important;
  padding:0!important;
  margin:0!important;
}
.v-sheet.v-theme--light.v-stepper.no-bg-stepper{
  width:100%!important;
  padding:0!important;
  margin:0!important;
}
.no-bg-stepper .v-stepper-header{
    background:#fff!important;
    color:#00000070!important;
}
.no-bg-stepper .v-stepper-header *{
    color:#00000090!important;
    font-weight:bold;
}
.no-bg-stepper .v-stepper-actions{
    display:none;

}
.v-sheet.v-theme--light.v-stepper.no-bg-stepper {
        box-shadow:none!important;
}
.data-popup-cloud-item-item{
    width:100%;
    height:30px;
    background:#01252a;
    border-radius:2px;
    padding:2px;
    color:#fff;
}
.mini-under-menu{
    background:#01252a;
    width:100%;
    height:45px;
    z-index:19;
    border-bottom-right-radius:12px;
    color:#fff;
    padding:10px;
    display:flex;
    align-items:flex-start;
    flex-direction:row;
    font-size:0.7em;
    overflow:auto;
}
.v-stepper-header{
  width:100%!important;
  padding:0!important;
  margin:0!important;
}
.card{
  box-sizing:border-box;
  overflow:auto;

  text-align:left;
}
.noradius{
  border-radius:0!important;
}

*{
  word-break:break-all;
}
.box{
  width:100%;
  padding:5px;
  text-align:left;
}

.form-text-input{
  border:solid 1px #00000020;
  border-radius:2px;
  font-size:0.8em;
  padding:5px;
  box-sizing: border-box;
  width:100%;
}
.form-text-input:disabled{
  background:#00000015!important;
}

.form-label{
  margin:0;
  padding:0;
  padding-block:2px;
  font-size:0.7em;
}
*{
  word-break:break-all;
}
.mini{
  font-size:0.7em;
}
</style>