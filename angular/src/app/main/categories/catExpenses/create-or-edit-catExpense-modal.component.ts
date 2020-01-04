import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { CatExpensesServiceProxy, CreateOrEditCatExpenseDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditCatExpenseModal',
    templateUrl: './create-or-edit-catExpense-modal.component.html'
})
export class CreateOrEditCatExpenseModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    catExpense: CreateOrEditCatExpenseDto = new CreateOrEditCatExpenseDto();



    constructor(
        injector: Injector,
        private _catExpensesServiceProxy: CatExpensesServiceProxy
    ) {
        super(injector);
    }

    show(catExpenseId?: number): void {

        if (!catExpenseId) {
            this.catExpense = new CreateOrEditCatExpenseDto();
            this.catExpense.id = catExpenseId;

            this.active = true;
            this.modal.show();
        } else {
            this._catExpensesServiceProxy.getCatExpenseForEdit(catExpenseId).subscribe(result => {
                this.catExpense = result.catExpense;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._catExpensesServiceProxy.createOrEdit(this.catExpense)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
