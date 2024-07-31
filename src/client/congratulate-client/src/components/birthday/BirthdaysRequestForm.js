import css from "../../css/birthday/BirthdaysRequestForm.module.css"

function BirthdaysRequestForm({params, setParams, onSubmit}) {
    const onMaxDaysLeftChanged = (e) => {
        const value = e.target.value;
        setParams({...params, maxDaysLeft: value});
    }

    const onOrderByChanged = (e) => {
        const value = e.target.value;
        setParams({...params, orderBy: value});
    }

    const onOrderByDescendingChanged = (e) => {
        const value = e.target.checked;
        setParams({...params, orderByDescending: value});
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        onSubmit();
    }

    return (
        <form className={css.form} onSubmit={handleSubmit}>
            <label className={css.label}>
                <span className={css.label_text}>Осталось дней:</span>
                <input className={css.input_number} type="number" value={params.maxDaysLeft} onChange={onMaxDaysLeftChanged}  min={0} max={365}></input>
                <input className={css.slider} type='range' value={params.maxDaysLeft} onChange={onMaxDaysLeftChanged} min={0} max={365} />
            </label>
            <label className={css.label}>
                <span className={css.label_text}>Сортировать по:</span>
                <select className={css.select} name="orderBy" value={params.orderBy} onChange={onOrderByChanged}>
                    <option value="personName">Имени</option>
                    <option value="personSurname">Фамилии</option>
                    <option value="daysLeft">Дате</option>
                </select>
            </label>
            <label className={css.label}>
                <input className={css.checkbox} type='checkbox' checked={params.orderByDescending} onChange={onOrderByDescendingChanged} />
                <span className={css.label_text}>по убыванию</span>
            </label>
            <button className={css.submit_btn} type='submit'>Обновить</button>
        </form>
    );
}

export default BirthdaysRequestForm;