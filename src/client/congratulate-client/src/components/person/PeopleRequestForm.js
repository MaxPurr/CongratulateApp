import css from "../../css/person/PeopleRequestForm.module.css";

function PeopleRequestForm({params, setParams, onSubmit}) {
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
        await onSubmit();
    }

    return (
        <form className={css.form} onSubmit={handleSubmit}>
                <label className={css.label}>
                    <span className={css.label_text}>Сортировать по:</span>
                    <select className={css.select} name="orderBy" value={params.orderBy} onChange={onOrderByChanged}>
                        <option value="name">Имени</option>
                        <option value="surname">Фамилии</option>
                        <option value="dayOfBirth">Дате рождения</option>
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

export default PeopleRequestForm;