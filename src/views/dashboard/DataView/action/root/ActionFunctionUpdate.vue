<template>
  <div :key="life.counterKey">
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in actionFunction.getInputs('text')" :key="param">
          <p class="form-label">
            {{actionFunction.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="actionFunction[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in actionFunction.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{actionFunction.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="actionFunction.getHeaders([param],'first')" class="form-text-input" v-model="actionFunction[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in actionFunction.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{actionFunction.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <Calendar :showIcon="true" :disabled="lock(param)" :key="actionFunction[param]" date-format="yy-mm-dd" v-model="actionFunction[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in actionFunction.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{actionFunction.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="actionFunction[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   actionFunction.create.includes(param) ? 'create' : '',
                                   actionFunction.select.includes(param) ? 'select' : '',
                                   actionFunction.delete.includes(param) ? 'delete' : '',
                                   actionFunction.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="actionFunction"
                               owner-key="actionFunction"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="actionFunction[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in actionFunction.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{actionFunction.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :owner="actionFunction" :key="actionFunction" :param="param" :disabled="lock(param)" :model="actionFunction"></SelectionManager>
          </div>
        </div>
        <div v-for="param in actionFunction.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{actionFunction.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="actionFunction.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="actionFunction.setFile($event, param)" />
          </div>
        </div>
        <div  style="display:block">
          <div  :hidden="hide(param)" class="box" v-for="param in actionFunction.getInputs('textAreaHeaders',null,true)" :key="param">
            <p class="form-label" >
              {{actionFunction.getHeaders([param],"first")}}
            </p>
            <InputText  :disabled="lock(param)" v-model="actionFunction[param]" class="form-text-input"/>
          </div>
          <div  :hidden="hide(param)" class="box" v-for="param in actionFunction.getInputs('textArea',null,true)" :key="param">
            <p class="form-label" >
              {{actionFunction.getHeaders([param],"first")}}
            </p>
            <Editor  :disabled="lock(param)" v-model="actionFunction[param]" class="form-text-input"/>
          </div>
        </div>
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
          {{ this.actionFunction.decycle() }}
        </div>
      </template>

      <template v-slot:item2contentRight>
        <div class="box">
          right
        </div>
      </template>

      <template v-slot:item3contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item3contentRight>
        <div class="box">
          right
        </div>
      </template>
    </DataPopup>
  </div>
</template>

<script>

import DataPopup from "@/components/popups/DataPopup.vue";

import { inject } from "vue";
import {ActionFunctionService} from "@/services/ActionFunctionService";
import Toast from "@/utils/classes/Toast";
import ActionFunctionCrudController from "@/views/dashboard/DataView/action/controllers/ActionFunctionCrudController";
import ActionFunctionClass from "@/views/dashboard/DataView/action/ActionFunctionClass";
import ActionFunctionLife from "@/views/dashboard/DataView/action/ActionFunctionLife";
import EditOptions from "@/views/dashboard/DataView/action/options/EditOptions";


export default {
  components: {DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      actionFunction: new ActionFunctionService(emitter),
    };
    const toast =new Toast();
    const crud = new ActionFunctionCrudController(services.actionFunction, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      actionFunction: new ActionFunctionClass(),
      life: new ActionFunctionLife(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.actionFunction = await this.crud.getById(this.$route.params.id)
    console.log('read actionFunction', this.actionFunction)
  },
  methods:{
    update(){
      console.log('up', this.actionFunction)
      this.crud.update(this.actionFunction).then(res=>{
        if(res?.invalid){
          console.log('invalid actionFunction create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.actionFunction[key] = collection
      }
    },
    lock(param){
      if(!this.actionFunction.$lock[param]){
        return false;
      }
      return this.actionFunction.$lock[param]()
    },
    hide(param){
      if(!this.actionFunction.$hide[param]){
        return false;
      }
      return this.actionFunction.$hide[param]()
    }
  },
  watch:{
    "actionFunction.systemFunction"(){
      this.life.counterKey++;
    },
    "actionFunction.storeModel"(){
      this.life.counterKey++;
    }
  }
}
</script>



<style>

</style>