import React from "react";
import { Box, Heading, Tab, Tabs } from "grommet";
import { Message, SignInForm, SignUpForm } from "components/signin";

export default function WelcomePage() {
	return (
		<Box align="center" pad="large">
			<Heading>Star Commander</Heading>
			<Tabs>
				<Tab title="Enlist">
					<Box pad="medium">
						<SignUpForm />
					</Box>
				</Tab>
				<Tab title="Engage">
					<Box pad="medium">
						<SignInForm />
					</Box>
				</Tab>
			</Tabs>
			<Message />
		</Box>
	);
}
