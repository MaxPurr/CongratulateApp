import { useState, useRef } from 'react';
import css from '../../css/person/PersonAddForm.module.css'

function PersonAddForm({onSubmit}) {
    const defaultPerson = {
        name: "",
        surname: "",
        dayOfBirth: new Date()
    };

    const [person, setPerson] = useState(defaultPerson);
    const [image, setImage] = useState(null);
    const imageInputRef = useRef(null);

    const onNameChanged = (e) => {
        const name = e.target.value;
        setPerson({...person, name})
    } 

    const onSurnameChanged = (e) => {
        const surname = e.target.value;
        setPerson({...person, surname})
    }

    const onDayOfBirthChanged = (e) => {
        const dayOfBirth = e.target.value;
        setPerson({...person, dayOfBirth })
    }

    const onImageChanged = (e) => {
        const file = e.target.files[0];
        console.log(e.target.files);
        setImage(file);
    }

    const clear = () => {
        setPerson(defaultPerson);
        setImage(null);
        imageInputRef.current.value = '';
    }

    const handleSubmit = async (e) => {
        clear();
        e.preventDefault();
        await onSubmit({person, image});
    }

    return (
        <form className={css.form} onSubmit={handleSubmit}>
            <div className={css.input_container}>
                <label className={css.label}>
                    <span className={css.label_title}>Имя:</span>
                    <input className={css.input} type="text" required value={person.name} onChange={onNameChanged} />
                </label>
                <label className={css.label}>
                    <span className={css.label_title}>Фамилия:</span>
                    <input className={css.input} type="text" required value={person.surname} onChange={onSurnameChanged} />
                </label>
                <label className={css.label}>
                    <span className={css.label_title}>Дата рождения:</span>
                    <input className={css.input} type="date" required value={person.dayOfBirth} onChange={onDayOfBirthChanged} />
                </label>
                <label className={css.label}>
                    <span className={css.label_title}>Фото:</span>
                    <input className={css.file_input} type="file" required accept="image/*" onChange={onImageChanged} ref={imageInputRef} />
                </label>
            </div>
            <button className={css.submit_bth} type="submit">Добавить</button>
        </form>
    );
}

export default PersonAddForm;