<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)"  v-for="param in status.getInputs('text')" :key="param">
          <p class="form-label">
            {{status.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="status[param]" class="form-text-input"/>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in status.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{status.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="status.getHeaders([param],'first')" class="form-text-input" v-model="status[param]"></InputNumber>
        </div>
        <div class="box" v-for="param in status.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{status.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="status[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in status.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{status.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="status[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   status.create.includes(param) ? 'create' : '',
                                   status.select.includes(param) ? 'select' : '',
                                   status.delete.includes(param) ? 'delete' : '',
                                   status.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="status"
                               owner-key="status"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="status[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in status.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{status.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="status" :param="param" :disabled="lock(param)" :model="status"></SelectionManager>
          </div>
        </div>
        <div v-for="param in status.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{status.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="status.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="status.setFile($event, param)" />
          </div>
        </div>
        <div v-for="param in status.getInputs('colors')" :key="param" :hidden="hide(param)"
             style="display:flex;justify-content:center;flex-direction:column;padding-inline:5px;">
          <p class="form-label">
            {{status.getHeaders([param],"first")}} <span class="mini"> | Color </span>
          </p>
          <div>
              <ColorPicker  v-model="status[param]"></ColorPicker>
          </div>
        </div>
      </template>

      <template v-slot:item2contentLeft>
          <div class="box">
            left
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
import {StatusService} from "@/services/StatusService";
import Toast from "@/utils/classes/Toast";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import StatusCrudController from "@/views/dashboard/DataView/status/controllers/StatusCrudController";
import StatusClass from "@/views/dashboard/DataView/status/StatusClass";
import CreateOptions from "@/views/dashboard/DataView/status/options/CreateOptions";
import StatusLife from "@/views/dashboard/DataView/status/StatusLife";



export default {
  components: {CollectionManager, DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      status: new StatusService(emitter)
    };
    const toast = new Toast()
    const crud = new StatusCrudController(services.status, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      status: new StatusClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new StatusLife()
    }
  },
  methods:{
    create(){
      console.log('status', this.status.decycle())
      this.crud.create(this.status.decycle()).then(res=>{
        if(res?.invalid){
          console.log('invalid status create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.status[key] = collection
      }
    },
    lock(param){
      if(!this.status.$lock[param]){
        return false;
      }
      return this.status.$lock[param]()
    },
    hide(param){
      if(!this.status.$hide[param]){
        return false;
      }
      return this.status.$hide[param]()
    }
  }
}
</script>



<style>

</style>