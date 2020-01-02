import React, { useCallback } from "react";
import { useDispatch } from "react-redux";
import { Button, Form } from "grommet";
import { FormFieldLabel, RequiredFieldLabel } from "components/forms";
import { actionCreators } from "store/auth";

export default function UpdateNameForm() {
	const dispatch = useDispatch();

	const onSubmit = useCallback(
		event => {
			const { callSign, password } = event.value;
			dispatch(actionCreators.updateName(callSign, password));
		},
		[dispatch]
	);

	return (
		<Form onSubmit={onSubmit}>
			<FormFieldLabel name="firstName" label="FirstName" required />
			<FormFieldLabel name="lastName" label="FastName" required />
			<Button type="submit" label="Update" primary />
			<RequiredFieldLabel />
		</Form>
	);
}
